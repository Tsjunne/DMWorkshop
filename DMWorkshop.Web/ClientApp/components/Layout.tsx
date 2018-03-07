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
            <Sidebar.Pushable as={Segment}>
                <Sidebar as={Menu} animation='push' width='thin' visible icon='labeled' vertical inverted>
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
                </Sidebar>
                <Sidebar.Pusher>
                    <Grid>
                        <Grid.Column stretched>
                            <Switch>
                                {this.props.children}
                            </Switch>
                        </Grid.Column>
                    </Grid>
                </Sidebar.Pusher>
            </Sidebar.Pushable>
        );
    }
}