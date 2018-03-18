import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { Card, Image, Icon, Label, Table, Button } from "semantic-ui-react";
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as Creatures from '../store/Creatures';
import * as Players from '../store/Players';
import * as Model from '../model/Creature';
import StatBlock from './StatBlock';

type CreatureCardProps =
    {
        creature: Model.Creature,
        addCreature: (creature: Model.Creature) => Creatures.AddCreatureAction
    }

export class CreatureCard extends React.Component<CreatureCardProps, {}> {

    public render() {
        return (
            <Card >
                <Card.Content>
                    <Image floated='left' size='mini' src={'/api/creatures/' + this.props.creature.name + '/portrait'} />
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
                        <StatBlock creature={this.props.creature} />
                    </Card.Description>
                </Card.Content>
            </Card>
        );
    }
}