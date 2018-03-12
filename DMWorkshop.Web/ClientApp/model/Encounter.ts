import * as Creature from '../model/Creature';
import * as CreatureInstance from '../model/CreatureInstance';

export interface EncounterData {
    instances: CreatureInstance.CreatureInstance[],
    totalXp: number,
    modifiedXp: number
}

export class Encounter {
    instances: CreatureInstance.CreatureInstance[];

    constructor(data: EncounterData) {
        this.instances = data.instances.map(x => x);
    }

    public buildData(): EncounterData {
        var totalXp = this.instances.filter(x => !(x instanceof CreatureInstance.Player)).map(x => x.creature.xp).reduce((a, b) => a + b, 0);
        return {
            instances: this.instances.sort((a, b) => b.initiative - a.initiative),
            totalXp: totalXp,
            modifiedXp: totalXp * this.calculateXpMultiplier()
        };
    }

    public addCreature(creature: Creature.Creature): Encounter {
        var instance = new CreatureInstance.CreatureInstance(creature);
        this.instances.push(instance);
        this.instances.sort((a, b) => b.initiative - a.initiative);

        return this;
    }

    public addPlayer(player: Creature.Creature): Encounter {
        var instance = new CreatureInstance.Player(player);
        this.instances.push(instance);
        this.instances.sort((a, b) => b.initiative - a.initiative);

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
        switch (true) {
            case this.instances.length >= 15: return 4; 
            case this.instances.length >= 11: return 3; 
            case this.instances.length >= 7: return 2.5; 
            case this.instances.length >= 3: return 2; 
            case this.instances.length >= 2: return 1.5; 
            default: return 1;
        }
    }
}