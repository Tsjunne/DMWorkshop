import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { Guid } from "guid-typescript";
import { AppThunkAction } from './';
import * as Creature from '../model/Creature';
import * as CreatureInstance from '../model/CreatureInstance';

export interface EncounterState {
    creatures: CreatureInstance.CreatureInstance[] 
}

enum ActionTypes {
    ADD_CREATURE = 'ADD_CREATURE',
    CHANGE_CREATURE_HP = 'CHANGE_CREATURE_HP',
    CHANGE_CREATURE_CONDITION = 'CHANGE_CREATURE_CONDITION',
    CLEAR_ENCOUNTER = 'CLEAR_ENCOUNTER'
}

interface AddCreatureAction {
    type: ActionTypes.ADD_CREATURE;
    creature: Creature.Creature;
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

type KnownAction = AddCreatureAction | ClearEncounterAction | ChangeCreatureHpAction | ChangeCreatureConditionAction;

export const actionCreators = {
    addCreature: (creature: Creature.Creature) => <AddCreatureAction> {
        type: ActionTypes.ADD_CREATURE,
        creature: creature
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

const unloadedState: EncounterState = { creatures: [] };

export const reducer: Reducer<EncounterState> = (state: EncounterState, action: KnownAction) => {
    switch (action.type) {
        case ActionTypes.ADD_CREATURE:
            return { creatures: state.creatures.concat(new CreatureInstance.CreatureInstance(action.creature)).sort((a, b) => b.initiative - a.initiative) };
        case ActionTypes.CLEAR_ENCOUNTER:
            return unloadedState;
        case ActionTypes.CHANGE_CREATURE_HP:
            return newState(state, action.instance, i => i.modifyHp(action.newHp));
        case ActionTypes.CHANGE_CREATURE_CONDITION:
            return newState(state, action.instance, i => action.add ? i.addCondition(action.condition) : i.removeCondition(action.condition));
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    // For unrecognized actions (or in cases where actions have no effect), must return the existing state
    //  (or default initial state if none was supplied)
    return state || unloadedState;
};

function newState(state: EncounterState, instance: CreatureInstance.CreatureInstance, modifier: (i: CreatureInstance.CreatureInstance) => CreatureInstance.CreatureInstance) : EncounterState {
    return {
        creatures: state.creatures.map(i => {
            if (i === instance)
                return modifier(i);
            else
                return i;
        })
    }
}