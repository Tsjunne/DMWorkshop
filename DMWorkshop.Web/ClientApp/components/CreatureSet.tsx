import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { Menu } from "semantic-ui-react";
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as Creatures from '../store/Creatures';
import { CreatureCard } from './CreatureCard';

type CreatureSetProps =
    Creatures.CreaturesState       
    & typeof Creatures.actionCreators    
    & RouteComponentProps<{ creatureSet: string }>; 

class CreatureSet extends React.Component<CreatureSetProps, Creatures.CreaturesState> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        //let creatureSet = this.props.match.params.creatureSet || 'All';
        this.props.requestCreatures('All');
    }

    componentWillReceiveProps(nextProps: CreatureSetProps) {
        // This method runs when incoming props (e.g., route params) change
        //let creatureSet = this.props.match.params.creatureSet || 'All';
        this.props.requestCreatures('All');
    }
    
    public render()
    {
        return (
            <div>
                    {this.props.creatures.map(creature =>
                        <CreatureCard key={creature.name} creature={creature} />
                    )}
            </div>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.creatures, // Selects which state properties are merged into the component's props
    Creatures.actionCreators                 // Selects which action creators are merged into the component's props
)(CreatureSet) as typeof CreatureSet;
