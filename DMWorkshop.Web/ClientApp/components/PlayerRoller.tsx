import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { List, Image, Icon, Button } from "semantic-ui-react";
import NumberPicker from 'semantic-ui-react-numberpicker';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as Creatures from '../store/Creatures';
import * as Players from '../store/Players';
import * as Model from '../model/Creature';

type PlayerRollerProps = {
    player: Model.Creature,
    onChange: (player: Model.Creature, value: number) => void
}

type PlayerRollerState = {
    roll: number
}

export class PlayerRoller extends React.Component<PlayerRollerProps, PlayerRollerState> {
    constructor(props: PlayerRollerProps) {
        super(props)

        this.state = { roll: 10}
    }

    onChange(e: any) {
        this.setState((prev, props) => ({ roll: e.value }));
        this.props.onChange(this.props.player, e.value);
    }

    public render() {
        return (
            <List.Item>
                <List.Content floated='right'>
                    <NumberPicker value={this.state.roll} min={0} max={30} onChange={(e: any) => this.onChange(e)} />
                </List.Content>
                <List.Content>
                    <Image avatar src={'/api/players/' + this.props.player.name + '/portrait'} />
                    {this.props.player.name}
                </List.Content>
            </List.Item>
        );
    }
}