import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { Card, Image, Icon, Label, Table, Button } from "semantic-ui-react";
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as Creatures from '../store/Creatures';
import * as Encounters from '../store/Encounters';

type CreatureCardProps =
    {
        creature: Creatures.Creature,
        addCreature: (creature: Creatures.Creature) => Creatures.AddCreatureAction
    }

export class CreatureCard extends React.Component<CreatureCardProps, {}> {

    public render() {
        return (
            <Card >
                <Card.Content>
                    <Image floated='left' size='mini' src={'/api/creatures/' + this.props.creature.name + '/image'} />
                        <Button floated='right' icon='plus' compact color='green' onClick={() => { this.props.addCreature(this.props.creature) }} />
                    <Card.Header>
                        <h4>{this.props.creature.name}</h4>
                    </Card.Header>
                    <Card.Meta>
                        <Table basic='very' fixed singleLine collapsing compact size='small'>
                            <Table.Body>
                                <Table.Row>
                                    <Table.Cell><Icon name='plus' /> {this.props.creature.maxHP}</Table.Cell>
                                    <Table.Cell><Icon name='shield' /> {this.props.creature.ac}</Table.Cell>
                                    <Table.Cell><Icon name='eye' /> {this.props.creature.passivePerception}</Table.Cell>
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
                                    <Table.Cell><b>{this.props.creature.modifiers[0]}</b></Table.Cell>
                                    <Table.Cell><b>{this.props.creature.modifiers[1]}</b></Table.Cell>
                                    <Table.Cell><b>{this.props.creature.modifiers[2]}</b></Table.Cell>
                                    <Table.Cell><b>{this.props.creature.modifiers[3]}</b></Table.Cell>
                                    <Table.Cell><b>{this.props.creature.modifiers[4]}</b></Table.Cell>
                                    <Table.Cell><b>{this.props.creature.modifiers[5]}</b></Table.Cell>
                                </Table.Row>
                            </Table.Body>
                        </Table>
                    </Card.Description>
                </Card.Content>
            </Card>
        );
    }
}