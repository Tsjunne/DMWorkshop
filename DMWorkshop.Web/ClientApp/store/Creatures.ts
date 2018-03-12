import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';
import * as Creature from '../model/Creature';

export interface CreaturesState {
    isLoading: boolean;
    creatureSet?: string;
    creatures: Creature.Creature[];
}

enum ActionTypes {
    REQUEST_CREATURES = 'REQUEST_CREATURES',
    RECEIVE_CREATURES = 'RECEIVE_CREATURES',
    ADD_CREATURE = 'ADD_CREATURE'
}

interface RequestCreaturesAction {
    type: ActionTypes.REQUEST_CREATURES;
    creatureSet: string;
}

interface ReceiveCreaturesAction {
    type: ActionTypes.RECEIVE_CREATURES;
    creatureSet: string;
    creatures: Creature.Creature[];
}

export interface AddCreatureAction {
    type: ActionTypes.ADD_CREATURE;
    creature: Creature.Creature;
}

type KnownAction = RequestCreaturesAction | ReceiveCreaturesAction | AddCreatureAction;

export const actionCreators = {
    requestCreatures: (creatureSet: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        if (creatureSet !== getState().creatures.creatureSet) {
            let fetchTask = fetch(`api/creatures?creatureSet=${creatureSet}`)
                .then(response => response.json() as Promise<Creature.Creature[]>)
                .then(data => {
                    dispatch({ type: ActionTypes.RECEIVE_CREATURES, creatureSet: creatureSet, creatures: data });
                });

            addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
            dispatch({ type: ActionTypes.REQUEST_CREATURES, creatureSet: creatureSet });
        }
    },

    addCreature: (creature: Creature.Creature) => <AddCreatureAction>{
        type: ActionTypes.ADD_CREATURE,
        creature: creature
    }
};

const unloadedState: CreaturesState = { creatures: [], isLoading: false };

export const reducer: Reducer<CreaturesState> = (state: CreaturesState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case ActionTypes.REQUEST_CREATURES:
            return {
                creatureSet: action.creatureSet,
                creatures: state.creatures,
                isLoading: true
            };
        case ActionTypes.RECEIVE_CREATURES:
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            if (action.creatureSet === state.creatureSet) {
                return {
                    creatureSet: action.creatureSet,
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

