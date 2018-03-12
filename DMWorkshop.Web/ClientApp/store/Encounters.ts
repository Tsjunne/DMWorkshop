import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { Guid } from "guid-typescript";
import { AppThunkAction } from './';
import * as Creature from '../model/Creature';
import * as CreatureInstance from '../model/CreatureInstance';
import * as Encounter from '../model/Encounter';

export interface EncounterState {
    encounter: Encounter.EncounterData
}

enum ActionTypes {
    ADD_CREATURE = 'ADD_CREATURE',
    ADD_PLAYER = 'ADD_PLAYER',
    CHANGE_CREATURE_HP = 'CHANGE_CREATURE_HP',
    CHANGE_CREATURE_CONDITION = 'CHANGE_CREATURE_CONDITION',
    CLEAR_ENCOUNTER = 'CLEAR_ENCOUNTER'
}

interface AddCreatureAction {
    type: ActionTypes.ADD_CREATURE;
    creature: Creature.Creature;
}

export interface AddPlayerAction {
    type: ActionTypes.ADD_PLAYER;
    player: Creature.Creature;
}

export interface ChangeCreatureHpAction {
    type: ActionTypes.CHANGE_CREATURE_HP;
    instance: CreatureInstance.CreatureInstance;
    newHp: number;
}

export interface ChangeCreatureConditionAction {
    type: ActionTypes.CHANGE_CREATURE_CONDITION;
    instance: CreatureInstance.CreatureInstance;
    condition: CreatureInstance.Condition;
    add: boolean;
}

interface ClearEncounterAction {
    type: ActionTypes.CLEAR_ENCOUNTER;
}

type KnownAction = AddCreatureAction | AddPlayerAction | ClearEncounterAction | ChangeCreatureHpAction | ChangeCreatureConditionAction;

export const actionCreators = {
    addCreature: (creature: Creature.Creature) => <AddCreatureAction>{
        type: ActionTypes.ADD_CREATURE,
        creature: creature
    },
    addPlayer: (player: Creature.Creature) => <AddPlayerAction>{
        type: ActionTypes.ADD_PLAYER,
        player: player
    },
    clearEncounter: () => <ClearEncounterAction> {
        type: ActionTypes.CLEAR_ENCOUNTER
    },
    changeCreatureHp: (instance: CreatureInstance.CreatureInstance, newHp: number) => <ChangeCreatureHpAction>{
        type: ActionTypes.CHANGE_CREATURE_HP,
        instance: instance,
        newHp: newHp
    },
    changeCreatureCondition: (instance: CreatureInstance.CreatureInstance, condition: CreatureInstance.Condition, add: boolean) => <ChangeCreatureConditionAction>{
        type: ActionTypes.CHANGE_CREATURE_CONDITION,
        instance: instance,
        condition: condition,
        add: add
    }
};

const unloadedState: EncounterState = {
    encounter: { instances: [], modifiedXp: 0, totalXp: 0 }
};

export const reducer: Reducer<EncounterState> = (state: EncounterState, action: KnownAction) => {
    switch (action.type) {
        case ActionTypes.ADD_CREATURE:
            return { encounter: new Encounter.Encounter(state.encounter).addCreature(action.creature).buildData() };
        case ActionTypes.ADD_PLAYER:
            return { encounter: new Encounter.Encounter(state.encounter).addPlayer(action.player).buildData() };
        case ActionTypes.CHANGE_CREATURE_HP:
            return { encounter: new Encounter.Encounter(state.encounter).changeHp(action.instance, action.newHp).buildData() };
        case ActionTypes.CHANGE_CREATURE_CONDITION:
            return { encounter: new Encounter.Encounter(state.encounter).changeCondition(action.instance, action.condition, action.add).buildData() };
        case ActionTypes.CLEAR_ENCOUNTER:
            return unloadedState;
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    // For unrecognized actions (or in cases where actions have no effect), must return the existing state
    //  (or default initial state if none was supplied)
    return state || unloadedState;
};