import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { Table } from "semantic-ui-react";
import { Slider } from 'react-semantic-ui-range';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as Encounter from '../model/Encounter';
import * as Encounters from '../store/Encounters';
import { CreatureInstance } from './CreatureInstance';

type EncounterProps =
    Encounters.EncounterState
    & typeof Encounters.actionCreators
    & RouteComponentProps<{ }>; 

class EncounterTracker extends React.Component<EncounterProps, {}> {
    public render() {
        return (
            <div style={{ minHeight: '99vh' }}>
                <Table>
                    <Table.Body>
                        <Table.Row>
                            <Table.Cell>Total XP: {this.props.encounter.totalXp} ({this.props.encounter.modifiedXp})</Table.Cell>
                        </Table.Row>
                    </Table.Body>
                </Table>
                <Table>
                    <Table.Body>
                        {this.props.encounter.instances.map(instance =>
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
)(EncounterTracker) as typeof EncounterTracker;
