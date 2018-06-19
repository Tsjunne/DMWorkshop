import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { Card, Image, Icon, Label, Table, Button } from "semantic-ui-react";
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as Creatures from '../store/Creatures';
import * as Players from '../store/Players';
import * as Model from '../model/Creature';
import { CreatureDetails } from './CreatureDetails';
import StatBlock from './StatBlock';

type PlayerCardProps =
    {
        player: Model.Creature
    }

export class PlayerCard extends React.Component<PlayerCardProps, {}> {

    public render() {
        return (
            <Card >
                <Card.Content>
                    <CreatureDetails creature={this.props.player} />
                    <Card.Header>
                        <h4>{this.props.player.name}</h4>
                    </Card.Header>
                    <Card.Meta>
                        <Table basic='very' fixed singleLine collapsing compact size='small'>
                            <Table.Body>
                                <Table.Row>
                                    <Table.Cell><Icon name='plus' /> {this.props.player.maxHP}</Table.Cell>
                                    <Table.Cell><Icon name='shield' /> {this.props.player.ac}</Table.Cell>
                                    <Table.Cell><Icon name='eye' /> {this.props.player.passivePerception}</Table.Cell>
                                </Table.Row>
                            </Table.Body>
                        </Table>
                    </Card.Meta>
                    <Card.Description>
                        <StatBlock creature={this.props.player} />
                    </Card.Description>
                </Card.Content>
            </Card>
        );
    }
}