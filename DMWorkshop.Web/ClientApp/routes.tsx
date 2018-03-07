import * as React from 'react';
import { Route, Redirect } from 'react-router-dom';
import { Segment } from "semantic-ui-react";
import Layout  from './components/Layout';
import CreatureSet from './components/CreatureSet';
import Encounter from './components/Encounter';
import UnderConstruction from './components/UnderConstruction';

export const routes = (
    <Layout >
        <Route path='/creatures/:creatureSet?' component={CreatureSet} />
        <Route path='/encounters/' component={Encounter} />
        <Route path='/party/' component={UnderConstruction} />
        <Redirect from="/" to="/creatures/" />
    </Layout>
);
