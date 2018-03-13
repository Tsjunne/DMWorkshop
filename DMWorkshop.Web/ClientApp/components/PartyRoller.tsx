import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { List, Modal, Button, Header, Icon } from "semantic-ui-react";
import { connect } from 'react-redux';
import * as Players from '../store/Players';
import * as Creature from '../model/Creature';
import * as Encounters from '../store/Encounters';
import { PlayerCard } from './PlayerCard';
import { PlayerRoller } from './PlayerRoller';

interface PartyRollerProps {
    icon: string,
    text: string,
    players: Creature.Creature[],
    onSubmit: (rolls: Creature.CharacterRoll[]) => void
}

interface PartyRollerState {
    showRollModal: boolean,
    rolls: Creature.CharacterRoll[]
}

export class PartyRoller extends React.Component<PartyRollerProps, PartyRollerState> {
    constructor(props: PartyRollerProps) {
        super(props)

        this.state = {
            showRollModal: false,
            rolls: props.players.map<Creature.CharacterRoll>(p => { return { character: p, roll: 10 } })
        }
    }

    componentWillReceiveProps(props: PartyRollerProps) {
        this.setState((prev, props) => ({
            showRollModal: false,
            rolls: props.players.map<Creature.CharacterRoll>(p => { return { character: p, roll: 10 } })
        }))
    }

    doRoll = () => this.setState((prev, props) => ({ showRollModal: true, rolls: prev.rolls }))

    cancelRoll = () => this.setState((prev, props) => ({ showRollModal: false, rolls: prev.rolls }))

    submitRoll = () => {
        this.setState((prev, props) => ({ showRollModal: false, rolls: prev.rolls }));
        this.props.onSubmit(this.state.rolls);
    }

    onChange = (player: Creature.Creature, value: number) => {
        var rolls = this.state.rolls.map(r => r.character == player ? { character: player, roll: value } : r);

        this.setState((prev: PartyRollerState, props) => ({
            showRollModal: prev.showRollModal,
            rolls: rolls
        }))
    }

    public render() {
        return (
            <Modal basic size='tiny'
                trigger={<Button icon={this.props.icon} color='yellow' onClick={this.doRoll}/>}
                open={this.state.showRollModal}
                onClose={() => this.cancelRoll()}
                >
                <Header icon={this.props.icon} content={this.props.text} />
                <Modal.Content>
                    <List>
                        {this.props.players.map(player =>
                            <PlayerRoller player={player} onChange={(p, v) => this.onChange(p, v)}/>
                        )}
                    </List>
                </Modal.Content>
                <Modal.Actions>
                    <Button icon={this.props.icon} color='green' onClick={() => this.submitRoll()}/>
                </Modal.Actions>
            </Modal>
        );
    }
}