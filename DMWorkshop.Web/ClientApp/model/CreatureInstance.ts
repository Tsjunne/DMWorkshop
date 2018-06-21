import * as Creature from '../model/Creature';
import { Guid } from "guid-typescript";

export enum Condition {
    Blinded ='Blinded',
    Charmed = 'Charmed',
    Deafened = 'Deafened',
    Frightened = 'Frightened',
    Grappled = 'Grappled',
    Incapacitated = 'Incapacitated',
    Invisible = 'Invisible',
    Paralyzed = 'Paralyzed',
    Petrified = 'Petrified',
    Poisoned = 'Poisoned',
    Prone = 'Prone',
    Restrained = 'Restrained',
    Stunned = 'Stunned',
    Unconscious = 'Unconscious',
    Exhaustion = 'Exhaustion'
}

export class CreatureInstance {
    id: string;
    creature: Creature.Creature
    initiative: number;
    hp: number;
    conditions: Condition[];
    number: number;
    maxHp: number;
    elite: boolean = false;

    constructor(creature: Creature.Creature) {
        this.id = Guid.create().toString();
        this.creature = creature;
        this.number = 1;
        this.initiative = this.roll(20) + creature.initiativeModifier;
        this.hp = creature.maxHP;
        this.maxHp = creature.maxHP;
        this.conditions = [];
    }

    private roll(die: number): number {
        return Math.floor(Math.random() * die) + 1
    }

    private averageRoll(die: number): number {
        return (die + 1) / 2;
    }
    
    protected clone(): CreatureInstance {
        let clone = new CreatureInstance(this.creature);
        clone.id = this.id;
        clone.number = this.number;
        clone.initiative = this.initiative;
        clone.hp = this.hp;
        clone.maxHp = this.maxHp;
        clone.conditions = this.conditions;
        clone.elite = this.elite;

        return clone;
    }

    get isPlayer() : boolean {
        return this instanceof Player;
    }

    public modifyHp(val: number): CreatureInstance {
        let clone = this.clone()
        clone.hp = val;

        return clone;
    }

    public addCondition(condition: Condition): CreatureInstance {
        if (this.conditions.some(c => c === condition)) return this;

        let clone = this.clone()
        clone.conditions = clone.conditions.concat(condition);

        return clone;
    }

    public removeCondition(condition: Condition): CreatureInstance {
        let clone = this.clone()
        clone.conditions = clone.conditions.filter(x => x !== condition);

        return clone;
    }

    public toggleElite(): CreatureInstance {
        let clone = this.clone()
        clone.elite = !clone.elite;

        let dmg = clone.maxHp - clone.hp
        clone.maxHp = Math.floor(this.creature.level * ((clone.elite ? this.creature.hitDie : this.averageRoll(this.creature.hitDie)) + this.creature.modifiers[Creature.Ability.Constitution]))
        clone.hp = Math.max(0, clone.maxHp - dmg);

        return clone;
    }
}

export class Player extends CreatureInstance {
    constructor(creature: Creature.Creature, initiative: number) {
        super(creature)

        this.initiative = initiative;
    }

    protected clone(): CreatureInstance {
        let clone = new Player(this.creature, this.initiative);
        clone.id = this.id;
        clone.hp = this.hp;
        clone.conditions = this.conditions;

        return clone;
    }
}
