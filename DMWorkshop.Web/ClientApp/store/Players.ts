import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';
import * as Creature from '../model/Creature';

export interface PlayersState {
    isLoading: boolean;
    party: string;
    players: Creature.Creature[];
}

enum ActionTypes {
    REQUEST_PLAYERS = 'REQUEST_PLAYERS',
    RECEIVE_PLAYERS = 'RECEIVE_PLAYERS',
    ADD_PLAYER = 'ADD_PLAYER'
}

interface RequestPlayersAction {
    type: ActionTypes.REQUEST_PLAYERS;
    party: string;
}

interface ReceivePlayersAction {
    type: ActionTypes.RECEIVE_PLAYERS;
    party: string;
    players: Creature.Creature[];
}

export interface AddPlayerAction {
    type: ActionTypes.ADD_PLAYER;
    player: Creature.Creature;
}

type KnownAction = RequestPlayersAction | ReceivePlayersAction | AddPlayerAction;

export const actionCreators = {
    requestPlayers: (party: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        if (party !== getState().players.party) {
            let fetchTask = fetch(`api/players?party=${party}`)
                .then(response => response.json() as Promise<Creature.Creature[]>)
                .then(data => {
                    dispatch({ type: ActionTypes.RECEIVE_PLAYERS, party: party, players: data });
                });

            addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
            dispatch({ type: ActionTypes.REQUEST_PLAYERS, party: party });
        }
    },

    addPlayer: (player: Creature.Creature) => <AddPlayerAction>{
        type: ActionTypes.ADD_PLAYER,
        player: player
    }
};

const unloadedState: PlayersState = { party: "", players: [], isLoading: false };

export const reducer: Reducer<PlayersState> = (state: PlayersState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case ActionTypes.REQUEST_PLAYERS:
            return {
                party: action.party,
                players: state.players,
                isLoading: true
            };
        case ActionTypes.RECEIVE_PLAYERS:
            return {
                party: action.party,
                players: action.players,
                isLoading: false
            };
        case ActionTypes.ADD_PLAYER:
            // Handled by another store
            break;
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};

