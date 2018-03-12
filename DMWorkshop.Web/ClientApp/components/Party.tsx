import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { Dimmer, Loader, Card } from "semantic-ui-react";
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as Players from '../store/Players';
import * as Encounters from '../store/Encounters';
import { CreatureCard } from './CreatureCard';

type PartyProps =
    Players.PlayersState       
    & typeof Players.actionCreators    
    & RouteComponentProps<{ creatureSet: string }>; 

class Party extends React.Component<PartyProps, Players.PlayersState> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        //let creatureSet = this.props.match.params.creatureSet || 'All';
        this.props.requestPlayers('All');
    }

    componentWillReceiveProps(nextProps: PartyProps) {
        // This method runs when incoming props (e.g., route params) change
        //let creatureSet = this.props.match.params.creatureSet || 'All';
        this.props.requestPlayers('All');
    }
    
    public render() {
        return (
            <div style={{ height: '99vh' }}>
                <Dimmer active={this.props.isLoading} inverted>
                    <Loader inverted>Loading</Loader>
                </Dimmer>
                <Card.Group>
                    {this.props.players.map(player =>
                        <CreatureCard key={player.name} creature={player} addCreature={this.props.addPlayer} />
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
