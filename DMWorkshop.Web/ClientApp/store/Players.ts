import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';
import * as Creature from '../model/Creature';
import * as Campaign from '../model/Campaign';

export interface PlayersState {
    isLoading: boolean;
    party: string;
    parties: Campaign.Party[];
    players: Creature.Creature[];
}

enum ActionTypes {
    REQUEST_PARTIES = 'REQUEST_PARTIES',
    RECEIVE_PARTIES = 'RECEIVE_PARTIES',
    REQUEST_PLAYERS = 'REQUEST_PLAYERS',
    RECEIVE_PLAYERS = 'RECEIVE_PLAYERS',
    ADD_PLAYERS = 'ADD_PLAYERS'
}

interface RequestPartiesAction {
    type: ActionTypes.REQUEST_PARTIES;
}

interface ReceivePartiesAction {
    type: ActionTypes.RECEIVE_PARTIES;
    parties: Campaign.Party[];
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

export interface AddPlayersAction {
    type: ActionTypes.ADD_PLAYERS;
    playerRolls: Creature.CharacterRoll[];
}

type KnownAction = RequestPartiesAction| ReceivePartiesAction | RequestPlayersAction | ReceivePlayersAction | AddPlayersAction;

export const actionCreators = {
    requestParties: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        if (getState().players.parties.length === 0) {
            let fetchTask = fetch(`api/parties`)
                .then(response => response.json() as Promise<Campaign.Party[]>)
                .then(data => {
                    dispatch({ type: ActionTypes.RECEIVE_PARTIES, parties: data });
                });

            addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
            dispatch({ type: ActionTypes.REQUEST_PARTIES });
        }
    },

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

    addPlayers: (playerRolls: Creature.CharacterRoll[]) => <AddPlayersAction>{
        type: ActionTypes.ADD_PLAYERS,
        playerRolls: playerRolls
    }
};

const unloadedState: PlayersState = {
    party: "",
    parties: [],
    players: [],
    isLoading: false
};

export const reducer: Reducer<PlayersState> = (state: PlayersState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case ActionTypes.REQUEST_PARTIES:
            return {
                party: state.party,
                parties: state.parties,
                players: state.players,
                isLoading: true
            }
        case ActionTypes.RECEIVE_PARTIES:
            return {
                party: state.party,
                parties: action.parties,
                players: state.players,
                isLoading: false
            }
        case ActionTypes.REQUEST_PLAYERS:
            return {
                party: action.party,
                parties: state.parties,
                players: state.players,
                isLoading: true
            };
        case ActionTypes.RECEIVE_PLAYERS:
            return {
                party: action.party,
                parties: state.parties,
                players: action.players,
                isLoading: false
            };
        case ActionTypes.ADD_PLAYERS:
            // Handled by another store
            break;
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};

