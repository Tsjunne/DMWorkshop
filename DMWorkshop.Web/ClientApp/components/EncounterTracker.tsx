import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { Table, Button, Icon, Image, Popup } from "semantic-ui-react";
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as Players from '../store/Players';
import * as Encounters from '../store/Encounters';
import { CreatureInstance } from './CreatureInstance';
import { PartyRoller } from './PartyRoller';

type EncounterProps =
    Encounters.EncounterState
    & Players.PlayersState
    & typeof Encounters.actionCreators
    & typeof Players.actionCreators
    & RouteComponentProps<{ }>; 

class EncounterTracker extends React.Component<EncounterProps, {}> {
    componentWillMount() {
        this.props.requestParties();
    }

    componentWillReceiveProps(nextProps: EncounterProps) {
        this.props.requestParties();
    }

    public render() {
        return (
            <div style={{ minHeight: '99vh' }}>
                <Table>
                    <Table.Body>
                        <Table.Row>
                            <Table.Cell collapsing><b>XP</b> {this.props.encounter.totalXp} ({this.props.encounter.modifiedXp})</Table.Cell>
                            <Table.Cell collapsing>
                                <Popup
                                    trigger={<Image size='mini' rounded src={'/images/' + this.props.encounter.difficulty + '.svg'}/>}
                                    content={this.props.encounter.difficulty}
                                />
                            </Table.Cell>
                            <Table.Cell />
                            <Table.Cell collapsing>
                                <PartyRoller icon='lightning' text='Roll Initiative!' players={this.props.players} onSubmit={this.props.addPlayers} />
                            </Table.Cell>
                            <Table.Cell collapsing>
                                <Popup
                                    trigger={
                                        <Button icon='trash' negative onClick={this.props.clearEncounter} />} 
                                    content='Clear encounter'
                                />
                            </Table.Cell>
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
    (state: ApplicationState) => Object.assign(state.encounters, state.players), // Selects which state properties are merged into the component's props
    Object.assign(Encounters.actionCreators, Players.actionCreators)                // Selects which action creators are merged into the component's props
)(EncounterTracker) as typeof EncounterTracker;
