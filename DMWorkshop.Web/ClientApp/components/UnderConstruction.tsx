import * as React from 'react';
import {  } from "semantic-ui-react";
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router-dom';
import { ApplicationState } from '../store';

export default class UnderConstruction extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return (
            <div>
                <h1>Under construction</h1>
            </div>
        );
    }
}