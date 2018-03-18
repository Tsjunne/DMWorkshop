
export interface Creature {
    name: string;
    maxHP: number;
    ac: number;
    xp: number;
    level: number;
    passivePerception: number;
    initiativeModifier: number;
    scores: number[];
    modifiers: number[];
}

export interface CharacterRoll {
    character: Creature,
    roll: number
}
