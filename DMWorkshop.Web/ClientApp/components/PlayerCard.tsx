import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { Card, Image, Icon, Label, Table, Button } from "semantic-ui-react";
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as Creatures from '../store/Creatures';
import * as Players from '../store/Players';
import * as Model from '../model/Creature';
import { CreatureDetails } from './CreatureDetails';

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
                        <Table basic='very' fixed singleLine collapsing compact size='small'>
                            <Table.Body>
                                <Table.Row>
                                    <Table.Cell><Image avatar src='/images/strength.svg' /></Table.Cell>
                                    <Table.Cell><Image avatar src='/images/dexterity.svg' /></Table.Cell>
                                    <Table.Cell><Image avatar src='/images/constitution.svg' /></Table.Cell>
                                    <Table.Cell><Image avatar src='/images/intelligence.svg' /></Table.Cell>
                                    <Table.Cell><Image avatar src='/images/wisdom.svg' /></Table.Cell>
                                    <Table.Cell><Image avatar src='/images/charisma.svg' /></Table.Cell>
                                </Table.Row>
                                <Table.Row textAlign='center'>
                                    <Table.Cell><b>{this.props.player.modifiers[0]}</b></Table.Cell>
                                    <Table.Cell><b>{this.props.player.modifiers[1]}</b></Table.Cell>
                                    <Table.Cell><b>{this.props.player.modifiers[2]}</b></Table.Cell>
                                    <Table.Cell><b>{this.props.player.modifiers[3]}</b></Table.Cell>
                                    <Table.Cell><b>{this.props.player.modifiers[4]}</b></Table.Cell>
                                    <Table.Cell><b>{this.props.player.modifiers[5]}</b></Table.Cell>
                                </Table.Row>
                            </Table.Body>
                        </Table>
                    </Card.Description>
                </Card.Content>
            </Card>
        );
    }
}