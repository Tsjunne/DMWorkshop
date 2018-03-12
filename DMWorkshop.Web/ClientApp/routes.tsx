import * as React from 'react';
import { Route, Redirect } from 'react-router-dom';
import { Segment } from "semantic-ui-react";
import Layout  from './components/Layout';
import CreatureSet from './components/CreatureSet';
import Party from './components/Party';
import EncounterTracker from './components/EncounterTracker';

export const routes = (
    <Layout >
        <Route path='/creatures/:creatureSet?' component={CreatureSet} />
        <Route path='/encounters/' component={EncounterTracker} />
        <Route path='/party/' component={Party} />
        <Redirect from="/" to="/creatures/" />
    </Layout>
);
