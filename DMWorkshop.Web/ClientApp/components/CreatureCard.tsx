import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as Creatures from '../store/Creatures';

type CreatureCardProps =
    {
        creature: Creatures.Creature
    }

export class CreatureCard extends React.Component<CreatureCardProps, {}> {

    public render() {
        return <h5>{this.props.creature.name}</h5>;
    }
}