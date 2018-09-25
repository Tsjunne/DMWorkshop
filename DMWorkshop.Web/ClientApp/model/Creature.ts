import { Condition } from "ClientApp/model/CreatureInstance";


export interface Creature {
    name: string;
    maxHP: number;
    ac: number;
    xp: number;
    cr: number;
    level: number;
    hitDie: number;
    size: string;
    passivePerception: number;
    initiativeModifier: number;
    scores: number[];
    modifiers: number[];
    speed: { [type: string]: number };
    skillModifiers: { [type: string]: number };
    savingThrows: { [type: string]: number };
    senses: { [type: string]: number };
    attacks: Attack[];
    specialAbilities: SpecialAbility[];
    conditionImmunities: Condition[];
    damageImmunities: DamageType[];
    damageResistances: DamageType[];
    damageVulnerabilities: DamageType[];
}

export interface CharacterRoll {
    character: Creature;
    roll: number;
}

export interface SpecialAbility {
    name: string;
    info: string;
}

export interface Attack {
    name: string;
    type: AttackType;
    hit: number;
    range: number;
    maxRange: number;
    info: string;
    damage: Damage[];
    reaction?: boolean;
    legendary?: boolean;
}

export interface Damage {
    type: DamageType;
    dieSize: number;
    dieCount: number;
    bonus: number;
    average: number;
}

export enum DamageType {
    Piercing = "piercing",
    Slashing = "slashing",
    Bludgeoning = "bludgeoning",
    Fire = "fire",
    Acid = "acid",
    Poison = "poison",
    Cold = "cold",
    Lightning = "lightning",
    Thunder = "thunder",
    Necrotic = "necrotic",
    Radiant = "radiant",
    NonMagical = "nonMagical",
    NonSilvered = "nonSilvered",
    NonAdamantine = "nonAdamantine"
}

export enum AttackType {
    Melee = 1 << 0,
    Ranged = 1 << 1,
    Weapon = 1 << 2,
    Spell = 1 << 3
}

export enum Ability {
    Strength,
    Dexterity,
    Constitution,
    Intelligence,
    Wisdom,
    Charisma
}
