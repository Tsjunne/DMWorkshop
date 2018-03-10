import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { Table } from "semantic-ui-react";
import { Slider } from 'react-semantic-ui-range';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as Encounters from '../store/Encounters';
import { CreatureInstance } from './CreatureInstance';

type EncounterProps =
    Encounters.EncounterState
    & typeof Encounters.actionCreators
    & RouteComponentProps<{ }>; 

class Encounter extends React.Component<EncounterProps, {}> {
    public render() {
        return (
            <div style={{ minHeight: '99vh' }}>
                <Table>
                    <Table.Body>
                        {this.props.creatures.map(instance =>
                            <CreatureInstance key={instance.id} instance={instance}
                                changeCreatureHp={this.props.changeCreatureHp}
                                changeCreatureCondition={this.props.changeCreatureCondition} />
                        )}
                    </Table.Body>
                </Table>
            </div>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.encounters, // Selects which state properties are merged into the component's props
    Encounters.actionCreators                 // Selects which action creators are merged into the component's props
)(Encounter) as typeof Encounter;
