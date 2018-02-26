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
        return <div className='card' >
            <div className="card-body">
                <h5 className="card-title">{this.props.creature.name}</h5>
                <p className="card-text">
                    
                </p>
            </div>
        </div>;
    }
}