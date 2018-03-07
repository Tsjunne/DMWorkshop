import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { Guid } from "guid-typescript";
import { AppThunkAction } from './';
import * as Creatures from './Creatures';

export interface EncounterState {
    creatures: CreatureInstance[] 
}

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
    creature: Creatures.Creature
    initiative: number;
    hp: number;
    conditions: Condition[];

    constructor(creature: Creatures.Creature) {
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
        return <CreatureInstance>{
            id: this.id,
            creature: this.creature,
            initiative: this.initiative,
            hp: this.hp,
            conditions : this.conditions
        }
    }

    public modifyHp(val: number): CreatureInstance {
        let clone = this.clone();
        clone.hp = val;

        return clone;
    }
}

enum ActionTypes {
    ADD_CREATURE = 'ADD_CREATURE',
    CHANGE_CREATURE_HP = 'CHANGE_CREATURE_HP',
    CLEAR_ENCOUNTER = 'CLEAR_ENCOUNTER'
}

interface AddCreatureAction {
    type: ActionTypes.ADD_CREATURE;
    creature: Creatures.Creature;
}

export interface ChangeCreatureHpAction {
    type: ActionTypes.CHANGE_CREATURE_HP;
    instance: CreatureInstance;
    newHp: number;
}

interface ClearEncounterAction {
    type: ActionTypes.CLEAR_ENCOUNTER;
}

type KnownAction = AddCreatureAction | ClearEncounterAction | ChangeCreatureHpAction;

export const actionCreators = {
    addCreature: (creature: Creatures.Creature) => <AddCreatureAction> {
        type: ActionTypes.ADD_CREATURE,
        creature: creature
    },
    clearEncounter: () => <ClearEncounterAction> {
        type: ActionTypes.CLEAR_ENCOUNTER
    },
    changeCreatureHp: (instance: CreatureInstance, newHp: number) => <ChangeCreatureHpAction>{
        type: ActionTypes.CHANGE_CREATURE_HP,
        instance: instance,
        newHp: newHp
    }
};

const unloadedState: EncounterState = { creatures: [] };

export const reducer: Reducer<EncounterState> = (state: EncounterState, action: KnownAction) => {
    switch (action.type) {
        case ActionTypes.ADD_CREATURE:
            return { creatures: state.creatures.concat(new CreatureInstance(action.creature)).sort((a, b) => b.initiative - a.initiative) };
        case ActionTypes.CLEAR_ENCOUNTER:
            return unloadedState;
        case ActionTypes.CHANGE_CREATURE_HP:
            return {
                creatures: state.creatures.map(instance => {
                    if (instance.id === action.instance.id)
                        return instance.modifyHp(action.newHp);
                    else
                        return instance;
                })
            }
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    // For unrecognized actions (or in cases where actions have no effect), must return the existing state
    //  (or default initial state if none was supplied)
    return state || unloadedState;
};
