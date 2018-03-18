import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { Table, Button, Icon, Image } from "semantic-ui-react";
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
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
                            <Table.Cell collapsing><b>XP</b> {this.props.encounter.totalXp} ({this.props.encounter.modifiedXp})</Table.Cell>
                            <Table.Cell collapsing>
                                <Image size='mini' rounded src={'/images/' + this.props.encounter.difficulty + '.svg'} />
                            </Table.Cell>
                            <Table.Cell />
                            <Table.Cell collapsing><Button icon='trash' negative onClick={this.props.clearEncounter}/></Table.Cell>
                        </Table.Row>
                    </Table.Body>
                </Table>
                <Table>
                    <Table.Body>
                        {this.props.encounter.instances.map(instance =>
                            <CreatureInstance key={instance.id} instance={instance}
                                changeCreatureHp={this.props.changeCreatureHp}
                                changeCreatureCondition={this.props.changeCreatureCondition}
                                removeCreature={this.props.removeCreature}/>
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
