import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { Card, Image, Icon, Label, Table, Button, Popup } from "semantic-ui-react";
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
                    <Popup
                        trigger={
                            <Button floated='right' icon='plus' compact color='green' onClick={() => { this.props.addCreature(this.props.creature) }} />}
                        content='Add creature to encounter'
                    />
                    <Card.Header>
                        <h4>{this.props.creature.name}</h4>
                    </Card.Header>
                    <Card.Meta>
                        <Icon name='trophy' /> {this.formatCR(this.props.creature.cr) + ' (' + this.props.creature.xp + ' XP)'}
                    </Card.Meta>
                </Card.Content>
            </Card>
        );
    }
    formatCR(cr: number): string {
        if (cr == 0.125) return '1/8';
        if (cr == 0.25) return '1/4';
        if (cr == 0.5) return '1/2';

        return cr.toString();
    }

}