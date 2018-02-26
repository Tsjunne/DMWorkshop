import * as React from 'react';
import { Segment, Sidebar, Menu, Icon } from "semantic-ui-react";
import { NavLink } from "react-router-dom";
import { Switch } from "react-router";
import { MainMenu } from './MainMenu';

export class Layout extends React.Component<{}, {}> {
    public render() {
        return (
            <div
                style={{
                    //see some additional required styles in site.css
                    display: "flex",
                    flexDirection: "column",
                    height: "100%"
                }}>
                { /* this is the currently selected view */}
                <div
                    style={{
                        flexGrow: 1,
                        overflowX: "hidden",
                        overflowY: "auto",
                    }}>
                    <Switch>
                        {this.props.children}
                    </Switch>
                </div>
                { /* this is the navigation fixed to the bottom */}
                <MainMenu style={{
                    flexShrink: 0, //don't allow flexbox to shrink it
                    borderRadius: 0, //clear semantic-ui style
                    margin: 0 //clear semantic-ui style
                }}/>
            </div>
        );
    }
}
