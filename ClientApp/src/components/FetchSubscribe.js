import React, { Component } from 'react';
import queryString from 'query-string'
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import ObjTable from './ObjTable'

const styles = () => ({
});

class FetchFetchSubscribe extends Component {
  static displayName = FetchFetchSubscribe.name;

  constructor(props) {
    super(props);
    this.state = { objs: [], loading: true };
  }

  componentDidMount() {
    this.getFetchSubscribes({});
  }

  handleDelete = (item) => {
    if (window.confirm("Really?"))
      this.deleteItem(item);
  }

  renderitemsTable(items) {
    return (
      <ObjTable objs={items} isLoading={this.state.isLoading} handleDelete={(item) => this.handleDelete(item)} />
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderitemsTable(this.state.objs);

    return (
      <div>
        {contents}
      </div>
    );
  }

  async getFetchSubscribes(filter) {
    var url = '/api/subscribes';
    if (null != filter) {
      url = `${url}?${queryString.stringify(filter)}`;
    }
    // this.setState({ ...this.state, loading: true });
    const response = await fetch(url);
    const data = await response.json();
    this.setState({ ...this.state, objs: data, loading: false });
  }

  async deleteItem(item) {
    let url = `/api/subscribes/${item.tag}`
    await fetch(url, {
      method: 'DELETE',
      headers: {
        'content-type': 'application/json'
      }//,
      // body: JSON.stringify(item)
    });
    let objs = this.state.objs;
    objs.splice(objs.indexOf(item), 1)
    this.setState({ ...this.state, objs: objs, loading: false });
  }
}

FetchFetchSubscribe.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(FetchFetchSubscribe);
