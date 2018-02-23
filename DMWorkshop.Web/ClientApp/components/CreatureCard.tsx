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
                    <table>
                        <tr>
                            <td>{this.props.creature.scores[0]}</td>
                            <td>{this.props.creature.scores[1]}</td>
                            <td>{this.props.creature.scores[2]}</td>
                            <td>{this.props.creature.scores[3]}</td>
                            <td>{this.props.creature.scores[4]}</td>
                            <td>{this.props.creature.scores[5]}</td>
                        </tr>
                    </table>
                </p>
            </div>
        </div>;
    }
}