import * as Creature from './Creature';
import * as CreatureInstance from './CreatureInstance';
import { xpThresholds } from './Tables';

export enum Difficulty {
    Critter = "Critter",
    Easy = "Easy",
    Medium = "Medium",
    Hard = "Hard",
    Deadly = "Deadly"
}

export interface EncounterData {
    instances: CreatureInstance.CreatureInstance[],
    totalXp: number,
    modifiedXp: number,
    difficulty: Difficulty
}

export class Encounter {
    instances: CreatureInstance.CreatureInstance[];

    constructor(data: EncounterData) {
        this.instances = data.instances.map(x => x);
    }

    public buildData(): EncounterData {
        var totalXp = this.instances.filter(x => !(x instanceof CreatureInstance.Player)).map(x => x.creature.xp).reduce((a, b) => a + b, 0);
        var modifiedXp = totalXp * this.calculateXpMultiplier();
        var difficulty = this.calculateDifficulty(modifiedXp);

        return {
            instances: this.instances.sort((a, b) => b.initiative - a.initiative),
            totalXp: totalXp,
            modifiedXp: modifiedXp,
            difficulty: difficulty
        };
    }

    public addCreature(creature: Creature.Creature): Encounter {
        var instance = new CreatureInstance.CreatureInstance(creature);

        var numbers = this.instances.filter(x => x.creature == creature).map(x => x.number);

        if (numbers.length > 0) {
            instance.number = 1 + Math.max(...numbers);
        }

        this.instances.push(instance);

        return this;
    }

    public addPlayers(playerRolls: Creature.CharacterRoll[]): Encounter {
        //Remove players from initative table
        this.instances = this.instances.filter(x => !(x instanceof CreatureInstance.Player))

        //Add players with new initiative
        playerRolls.map(x => {
            var instance = new CreatureInstance.Player(x.character, x.roll);
            this.instances.push(instance);
        });
        
        return this;
    }

    public changeHp(instance: CreatureInstance.CreatureInstance, newHp: number): Encounter {
        return this.changeInstance(instance, i => i.modifyHp(newHp));
    }

    public changeCondition(instance: CreatureInstance.CreatureInstance, condition: CreatureInstance.Condition, add: boolean): Encounter {
        return this.changeInstance(instance, i => add ? i.addCondition(condition) : i.removeCondition(condition));
    }

    changeInstance(instance: CreatureInstance.CreatureInstance, modifier: (i: CreatureInstance.CreatureInstance) => CreatureInstance.CreatureInstance): Encounter {
        this.instances = this.instances.map(i => {
            if (i === instance)
                return modifier(i);
            else
                return i;
        });

        return this;
    }

    calculateXpMultiplier() {
        var creatures = this.instances.filter(x => !x.isPlayer).length;

        switch (true) {
            case creatures >= 15: return 4; 
            case creatures >= 11: return 3; 
            case creatures >= 7: return 2.5; 
            case creatures >= 3: return 2; 
            case creatures >= 2: return 1.5; 
            default: return 1;
        }
    }

    calculateDifficulty(modifiedXp: number): Difficulty {
        var thresholds = this.instances.filter(x => x.isPlayer).map(x => xpThresholds[x.creature.level])
        var partyThreshold = thresholds.reduce(function (sum, t) { return { easy: sum.easy + t.easy, medium: sum.medium + t.medium, hard: sum.hard + t.hard, deadly: sum.deadly + t.deadly } });

        switch (true) {
            case modifiedXp >= partyThreshold.deadly:
                return Difficulty.Deadly;
            case modifiedXp >= partyThreshold.hard:
                return Difficulty.Hard;
            case modifiedXp >= partyThreshold.medium:
                return Difficulty.Medium;
            case modifiedXp >= partyThreshold.easy:
                return Difficulty.Easy;
            default:
                return Difficulty.Critter;
        }
    }
}