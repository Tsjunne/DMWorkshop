import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

export interface CreaturesState {
    isLoading: boolean;
    creatureSet?: string;
    creatures: Creature[];
}

export interface Creature {
    name: string;
    maxHP: number;
    ac: number;
    xp: number;
    passivePerception: number;
    scores: number[];
    modifiers: number[];
}

enum ActionTypes {
    REQUEST_CREATURES,
    RECEIVE_CREATURES
}

interface RequestCreaturesAction {
    type: ActionTypes.REQUEST_CREATURES;
    creatureSet: string;
}

interface ReceiveCreaturesAction {
    type: ActionTypes.RECEIVE_CREATURES;
    creatureSet: string;
    creatures: Creature[];
}

type KnownAction = RequestCreaturesAction | ReceiveCreaturesAction;

export const actionCreators = {
    requestCreatures: (creatureSet: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        if (creatureSet !== getState().creatures.creatureSet) {
            let fetchTask = fetch(`api/Creatures?creatureSet=${creatureSet}`)
                .then(response => response.json() as Promise<Creature[]>)
                .then(data => {
                    dispatch({ type: ActionTypes.RECEIVE_CREATURES, creatureSet: creatureSet, creatures: data });
                });

            addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
            dispatch({ type: ActionTypes.REQUEST_CREATURES, creatureSet: creatureSet });
        }
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
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};

