﻿
export interface Creature {
    name: string;
    maxHP: number;
    ac: number;
    xp: number;
    cr: number;
    level: number;
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
    conditionImmunities: string[];
    damageImmunities: string[];
    damageResistances: string[];
    damageVulnerabilities: string[];
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
}

export interface Damage {
    type: DamageType;
    dieSize: number;
    dieCount: number;
    bonus: number;
    average: number;
}

export enum DamageType {
    Piercing = "Piercing",
    Slashing = "Slashing",
    Bludgeoning = "Bludgeoning",
    Fire = "Fire",
    Acid = "Acid",
    Poison = "Poison",
    Cold = "Cold",
    Lightning = "Lightning",
    Thunder = "Thunder",
    Necrotic = "Necrotic",
    Radiant = "Radiant",
    NonMagical = "NonMagical"
}

export enum AttackType {
    Melee = 1 << 0,
    Ranged = 1 << 1,
    Weapon = 1 << 2,
    Spell = 1 << 3
}
