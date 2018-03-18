import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { Dimmer, Loader, Card, Modal, Button, Header, Icon, List, Table } from "semantic-ui-react";
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as Players from '../store/Players';
import * as Creature from '../model/Creature';
import * as Encounters from '../store/Encounters';
import { PlayerCard } from './PlayerCard';
import { PartyRoller } from './PartyRoller';

type PartyProps =
    Players.PlayersState       
    & typeof Players.actionCreators    
    & RouteComponentProps<{ creatureSet: string }>; 

interface PartyState {
    showRollInitiativeModal: boolean,
    initiativeResult: PlayerRoll[]
}

interface PlayerRoll {
    player: Creature.Creature,
    initiative: number
}

class Party extends React.Component<PartyProps, PartyState> {
    constructor(props: PartyProps) {
        super(props)

        this.state = {
            showRollInitiativeModal: false,
            initiativeResult: props.players.map<PlayerRoll>(p => { return { player: p, initiative: 10 } })
        }
    }

    componentWillMount() {
        // This method runs when the component is first added to the page
        //let creatureSet = this.props.match.params.creatureSet || 'All';
        this.props.requestPlayers('LMoP');
    }

    componentWillReceiveProps(nextProps: PartyProps) {
        // This method runs when incoming props (e.g., route params) change
        //let creatureSet = this.props.match.params.creatureSet || 'All';
        this.props.requestPlayers('LMoP');
    }
    
    public render() {
        return (
            <div style={{ height: '99vh' }}>
                <Dimmer active={this.props.isLoading} inverted>
                    <Loader inverted>Loading</Loader>
                </Dimmer>
                <Table>
                    <Table.Body>
                        <Table.Row>
                            <Table.Cell></Table.Cell>
                            <Table.Cell collapsing>
                                <PartyRoller icon='lightning' text='Roll Initiative!' players={this.props.players} onSubmit={this.props.addPlayers} /></Table.Cell>
                        </Table.Row>
                    </Table.Body>
                </Table>
                <Card.Group>
                    {this.props.players.map(player =>
                        <PlayerCard key={player.name} player={player} />
                    )}
                </Card.Group>
            </div>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.players, 
    Players.actionCreators
)(Party) as typeof Party;
