import * as React from 'react';
import { Grid, Segment, Sidebar, Menu, Icon, Label } from "semantic-ui-react";
import { NavLink } from "react-router-dom";
import { Switch, RouteComponentProps } from "react-router";
import { ApplicationState } from '../store';
import { connect } from 'react-redux';
import * as Encounters from '../store/Encounters';

class Layout extends React.Component<ApplicationState, {}> {
    public render() {
        return (
            <Grid>
                <Grid.Column stretched width={2}>
                    <Menu width='thin' icon='labeled' vertical inverted>
                        <Menu.Item as={NavLink} to={`/creatures/${this.props.creatures.monsterList}`}>
                            <Icon name="id card" />
                            Bestiary
					    </Menu.Item>
                        <Menu.Item as={NavLink} to={`/party/${this.props.players.party}`}>
                            <Icon name="group" />
                            Party
                        </Menu.Item>
                        <Menu.Item as={NavLink} to="/encounter/">
                            <Label color='red'>{this.props.encounters.encounter.instances.length}</Label>
                            <Icon name="sort content descending" />
                            Encounter
					    </Menu.Item>
                    </Menu>
                </Grid.Column>

                <Grid.Column stretched width={14}>
                    <Switch>
                        {this.props.children}
                    </Switch>
                </Grid.Column>
            </Grid>
        );
    }
}


export default connect(
    (state: ApplicationState) => state, // Selects which state properties are merged into the component's props
    Encounters.actionCreators                 // Selects which action creators are merged into the component's props
)(Layout) as typeof Layout;
