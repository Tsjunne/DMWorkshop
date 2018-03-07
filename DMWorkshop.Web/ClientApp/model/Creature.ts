
export interface Creature {
    name: string;
    maxHP: number;
    ac: number;
    xp: number;
    passivePerception: number;
    initiativeModifier: number;
    scores: number[];
    modifiers: number[];
}
