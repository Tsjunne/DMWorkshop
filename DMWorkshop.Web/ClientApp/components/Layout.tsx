import * as React from 'react';
import { Grid, Segment, Sidebar, Menu, Icon, Label } from "semantic-ui-react";
import { NavLink } from "react-router-dom";
import { Switch, RouteComponentProps } from "react-router";
import { ApplicationState } from '../store';
import { connect } from 'react-redux';
import * as Encounters from '../store/Encounters';

export default class Layout extends React.Component<{}, {}> {
    public render() {
        return (
            <Grid>
                <Grid.Column stretched width={1}>
                    <Menu width='thin'  icon='labeled' vertical inverted>
                        <Menu.Item as={NavLink} to="/creatures/">
                            <Icon name="id card" />
                            Creatures
					</Menu.Item>
                        <Menu.Item as={NavLink} to="/encounters/">
                            <Icon name="sort content descending" />
                            Encounters
					</Menu.Item>
                        <Menu.Item as={NavLink} to="/party/">
                            <Icon name="group" />
                            Party
					</Menu.Item>
                    </Menu>
                </Grid.Column>

                <Grid.Column stretched width={15}>
                        <Switch>
                            {this.props.children}
                        </Switch>
                </Grid.Column>
            </Grid>
        );
    }
}