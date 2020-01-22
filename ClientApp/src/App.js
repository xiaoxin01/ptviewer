import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import FetchItem from './components/FetchItem'
import FetchFavorite from './components/FetchFavorite'

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path='/' component={FetchItem} />
        <Route exact path='/favorate' component={FetchFavorite} />
      </Layout>
    );
  }
}
