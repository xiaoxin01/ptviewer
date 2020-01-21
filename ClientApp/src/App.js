import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import FetchItem from './components/FetchItem'

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path='/' component={FetchItem} />
      </Layout>
    );
  }
}
