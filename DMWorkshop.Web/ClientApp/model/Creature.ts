
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
    vision: { [type: string]: number };
}

export interface CharacterRoll {
    character: Creature,
    roll: number
}
