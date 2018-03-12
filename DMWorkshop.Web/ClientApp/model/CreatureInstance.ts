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
    Paralized = 'Paralized',
    Petrified = 'Petrified',
    Poisoned = 'Poisoned',
    Prone = 'Prone',
    Restrained = 'Restrained',
    Stunned = 'Stunned',
    Unconscious = 'Unconscious'
}

export class CreatureInstance {
    id: string;
    creature: Creature.Creature
    initiative: number;
    hp: number;
    conditions: Condition[];
    number: number;

    constructor(creature: Creature.Creature) {
        this.id = Guid.create().toString();
        this.creature = creature;
        this.initiative = this.roll(20) + creature.initiativeModifier;
        this.hp = creature.maxHP;
        this.conditions = [];
    }

    private roll(die: number): number {
        return Math.floor(Math.random() * die) + 1
    }

    protected clone(): CreatureInstance {
        let clone = new CreatureInstance(this.creature);
        clone.id = this.id;
        clone.initiative = this.initiative;
        clone.hp = this.hp;
        clone.conditions = this.conditions;

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
}

export class Player extends CreatureInstance {
    protected clone(): CreatureInstance {
        let clone = new Player(this.creature);
        clone.id = this.id;
        clone.initiative = this.initiative;
        clone.hp = this.hp;
        clone.conditions = this.conditions;

        return clone;
    }
}
