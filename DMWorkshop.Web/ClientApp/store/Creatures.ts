import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';
import * as Creature from '../model/Creature';
import * as Campaign from '../model/Campaign';

export interface CreaturesState {
    isLoading: boolean;
    monsterList?: string;
    creatures: Creature.Creature[];
    monsterLists: Campaign.CreatureList[];
}

enum ActionTypes {
    REQUEST_MONSTERLISTS = 'REQUEST_MONSTERLISTS',
    RECEIVE_MONSTERLISTS = 'RECEIVE_MONSTERLISTS',
    REQUEST_CREATURES = 'REQUEST_CREATURES',
    RECEIVE_CREATURES = 'RECEIVE_CREATURES',
    ADD_CREATURE = 'ADD_CREATURE'
}

interface RequestMonsterListsAction {
    type: ActionTypes.REQUEST_MONSTERLISTS;
}

interface ReceiveMonsterListsAction {
    type: ActionTypes.RECEIVE_MONSTERLISTS;
    monsterLists: Campaign.CreatureList[];
}


interface RequestCreaturesAction {
    type: ActionTypes.REQUEST_CREATURES;
    monsterList: string;
}

interface ReceiveCreaturesAction {
    type: ActionTypes.RECEIVE_CREATURES;
    monsterList: string;
    creatures: Creature.Creature[];
}

export interface AddCreatureAction {
    type: ActionTypes.ADD_CREATURE;
    creature: Creature.Creature;
}

type KnownAction = RequestMonsterListsAction | ReceiveMonsterListsAction | RequestCreaturesAction | ReceiveCreaturesAction | AddCreatureAction;

export const actionCreators = {
    requestMonsterLists: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        if (getState().creatures.monsterLists.length === 0) {
            let fetchTask = fetch(`api/monsterLists`)
                .then(response => response.json() as Promise<Campaign.CreatureList[]>)
                .then(data => {
                    dispatch({ type: ActionTypes.RECEIVE_MONSTERLISTS, monsterLists: data });
                });

            addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
            dispatch({ type: ActionTypes.REQUEST_MONSTERLISTS });
        }
    },

    requestCreatures: (monsterList: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        if (monsterList !== getState().creatures.monsterList) {
            let fetchTask = fetch(`api/creatures?monsterList=${monsterList}`)
                .then(response => response.json() as Promise<Creature.Creature[]>)
                .then(data => {
                    dispatch({ type: ActionTypes.RECEIVE_CREATURES, monsterList: monsterList, creatures: data });
                });

            addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
            dispatch({ type: ActionTypes.REQUEST_CREATURES, monsterList: monsterList });
        }
    },

    addCreature: (creature: Creature.Creature) => <AddCreatureAction>{
        type: ActionTypes.ADD_CREATURE,
        creature: creature
    }
};

const unloadedState: CreaturesState = {
    monsterList: "",
    monsterLists: [],
    creatures: [],
    isLoading: false
};

export const reducer: Reducer<CreaturesState> = (state: CreaturesState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case ActionTypes.REQUEST_MONSTERLISTS:
            return {
                monsterList: state.monsterList,
                monsterLists: state.monsterLists,
                creatures: state.creatures,
                isLoading: true
            }
        case ActionTypes.RECEIVE_MONSTERLISTS:
            return {
                monsterList: state.monsterList,
                monsterLists: action.monsterLists,
                creatures: state.creatures,
                isLoading: false
            }
        case ActionTypes.REQUEST_CREATURES:
            return {
                monsterList: action.monsterList,
                monsterLists: state.monsterLists,
                creatures: state.creatures,
                isLoading: true
            };
        case ActionTypes.RECEIVE_CREATURES:
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            if (action.monsterList === state.monsterList) {
                return {
                    monsterList: action.monsterList,
                    monsterLists: state.monsterLists,
                    creatures: action.creatures,
                    isLoading: false
                };
            }
            break;
        case ActionTypes.ADD_CREATURE:
            // Handled by another store
            break;
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};

