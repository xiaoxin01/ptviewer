import React, { Component } from 'react';
import queryString from 'query-string'
import ReactPaginate from 'react-paginate';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';

const styles = theme => ({
  root: {
    display: 'flex',
    flexWrap: 'wrap',
  },
  formControl: {
    margin: theme.spacing.unit,
    minWidth: 120,
  },
  selectEmpty: {
    marginTop: theme.spacing.unit * 2,
  },
});

class FetchItem extends Component {
  static displayName = FetchItem.name;

  constructor(props) {
    super(props);
    this.state = { items: [], loading: true, search: "", page: 1 };
  }

  componentDidMount() {
    this.populateWeatherData({});
  }

  handleChange = (e) => {
    if (e.key === "Enter") {
      this.populateWeatherData({ search: e.target.value, source: this.state.source });
    } else {
      this.setState({ search: e.target.value })
    }
  }

  handlePageChange = page => {
    this.populateWeatherData({ search: this.state.search, page: page.selected + 1, source: this.state.source });
  }

  handleSourceChange = event => {
    this.populateWeatherData({ search: this.state.search, page: 1, source: event.target.value });
  }

  static renderitemsTable(items) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Title</th>
            <th>Score</th>
            <th>Size</th>
            <th>Source</th>
            <th>Created</th>
          </tr>
        </thead>
        <tbody>
          {items.map(item =>
            <tr key={item.id}>
              <td><a href={`open?id=${item.id}`} target="_blank" rel="noopener noreferrer">{item.title}</a><br />{item.description.replace(item.title, "")}</td>
              <td><a href={item.movieUrl} target="_blank" rel="noopener noreferrer">{item.movieScore}</a></td>
              <td>{item.size}</td>
              <td>{item.source}</td>
              <td>{item.created}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  renderitemsPagination(page) {
    return (
      <ReactPaginate containerClassName={'pagination'}
        pageClassName={'page-item'}
        pageLinkClassName={'page-link'}
        activeClassName={'active'}
        onPageChange={this.handlePageChange}
        forcePage={page - 1} pageCount={10} pageRangeDisplayed={2} marginPagesDisplayed={3}>
      </ReactPaginate>
    );
  }

  renderSources(source) {
    const { classes } = this.props;

    return (
      <form autoComplete="off">
        <FormControl className={classes.formControl}>
          <InputLabel htmlFor="age-simple">Source</InputLabel>
          <Select
            value={source}
            onChange={this.handleSourceChange}
            inputProps={{
              name: 'source',
              id: 'source',
            }}
          >
            <MenuItem value="">
              <em>None</em>
            </MenuItem>
            <MenuItem value="hds">hds</MenuItem>
            <MenuItem value="mt_a">mt_a</MenuItem>
            <MenuItem value="mt_s">mt_s</MenuItem>
          </Select>
        </FormControl>
      </form>
    )
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchItem.renderitemsTable(this.state.items);

    return (
      <div>
        <div><input type="text" onKeyPress={this.handleChange} /></div>
        {this.renderSources(this.state.source)}
        {this.renderitemsPagination(this.state.page)}
        {contents}
      </div>
    );
  }

  async populateWeatherData(filter) {
    var url = '/api/items';
    var { page = 1, source = "", search = "" } = filter;
    if (null != filter) {
      url = `${url}?${queryString.stringify(filter)}`;
    }
    // this.setState({ ...this.state, loading: true });
    const response = await fetch(url);
    const data = await response.json();
    this.setState({ ...this.state, items: data, loading: false, page: page, source: source, search: search });
  }
}

FetchItem.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(FetchItem);