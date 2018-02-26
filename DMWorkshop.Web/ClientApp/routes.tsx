import * as React from 'react';
import { Route, Redirect } from 'react-router-dom';
import { Layout } from './components/Layout';
import CreatureSet from './components/CreatureSet';

export const routes = (
    <Layout>
        <Route path='/creatures/:creatureSet?' component={CreatureSet} />
        <Redirect from="/" to="/creatures/" />
    </Layout>
);
