import * as Creature from '../model/Creature';
import { Guid } from "guid-typescript";

export enum Condition {
    Blinded,
    Charmed,
    Deafened,
    Frightened,
    Grappeled,
    Incapacitated,
    Invisible,
    Paralized,
    Petrified,
    Poisened,
    Prone,
    Restrained,
    Stunned,
    Unconscious
}

export class CreatureInstance {
    id: string;
    creature: Creature.Creature
    initiative: number;
    hp: number;
    conditions: Condition[];

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

    private clone(): CreatureInstance {
        let clone = new CreatureInstance(this.creature);
        clone.id = this.id;
        clone.initiative = this.initiative;
        clone.hp = this.hp;
        clone.conditions = this.conditions;

        return clone;
    }

    public modifyHp(val: number): CreatureInstance {
        let clone = this.clone()
        clone.hp = val;

        return clone;
    }
}
