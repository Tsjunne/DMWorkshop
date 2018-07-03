import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { Dimmer, Loader, Card, Table, Button } from "semantic-ui-react";
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as Creatures from '../store/Creatures';
import * as Encounters from '../store/Encounters';
import { CreatureCard } from './CreatureCard';

type CreatureSetProps =
    Creatures.CreaturesState       
    & typeof Creatures.actionCreators    
    & RouteComponentProps<{ monsterList: string }>; 

class CreatureSet extends React.Component<CreatureSetProps, Creatures.CreaturesState> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        this.props.requestMonsterLists();
        let monsterList = this.props.match.params.monsterList || '';
        this.props.requestCreatures(monsterList);
    }

    componentWillReceiveProps(nextProps: CreatureSetProps) {
        // This method runs when incoming props (e.g., route params) change
        let monsterList = nextProps.match.params.monsterList || '';
        this.props.requestCreatures(monsterList);
    }

    public render() {
        return (
            <div style={{ height: '99vh' }}>
                <Table compact>
                    <Table.Body>
                        <Table.Row>
                            <Table.Cell>
                                <Button.Group>
                                    {this.props.monsterLists.map(monsterList =>
                                        <Button as={Link} content={monsterList.name} basic active={this.props.monsterList === monsterList.name} to={`/creatures/${monsterList.name}`} />
                                    )}
                                </Button.Group>
                            </Table.Cell>
                        </Table.Row>
                    </Table.Body>
                </Table>
                <Card.Group>
                    {this.props.creatures.map(creature =>
                        <CreatureCard key={creature.name} creature={creature} addCreature={this.props.addCreature} />
                    )}
                </Card.Group>
                <Dimmer active={this.props.isLoading} inverted>
                    <Loader inverted>Loading</Loader>
                </Dimmer>
            </div>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.creatures, // Selects which state properties are merged into the component's props
    Creatures.actionCreators                 // Selects which action creators are merged into the component's props
)(CreatureSet) as typeof CreatureSet;
