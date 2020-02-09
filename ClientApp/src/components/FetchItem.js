import React, { Component } from 'react';
import queryString from 'query-string'
import ReactPaginate from 'react-paginate';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import Button from '@material-ui/core/Button';
import ButtonGroup from '@material-ui/core/ButtonGroup';
import IconButton from '@material-ui/core/IconButton';
import ClearIcon from '@material-ui/icons/Clear';
import ItemsTable from './ItemsTable'
import TurnedInNotIcon from '@material-ui/icons/TurnedInNot';

const styles = theme => ({
});

class FetchItem extends Component {
  static displayName = FetchItem.name;

  constructor(props) {
    super(props);
    this.state = { items: [], hots: [], loading: true, search: "", page: 1 };
  }

  componentDidMount() {
    this.getItems({ source: "mt_a" });
    this.getHotsData()
  }

  handleInput = (e) => {
    this.setState({ search: e.target.value })
  }
  handlePress = (e) => {
    if (e.key === "Enter") {
      this.getItems({ search: e.target.value, source: this.state.source });
    }
  }

  handlePageChange = page => {
    this.getItems({ search: this.state.search, page: page.selected + 1, source: this.state.source });
  }

  handleSourceChange = event => {
    this.getItems({ search: this.state.search, page: 1, source: event.target.value });
  }

  handleHotClick = event => {
    this.getItems({ search: event.target.innerText, source: this.state.source });
  }

  handleSubscribeClick = event => {
    this.subscribe(this.state.search)
  }

  renderitemsTable(items) {
    return (
      <ItemsTable items={items} isLoading={this.state.isLoading}></ItemsTable>
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

  renderHots(hots) {
    return (
      <ButtonGroup variant="text" color="primary" aria-label="text primary button group">
        <IconButton aria-label="delete" title="clear" onClick={this.handleHotClick}>
          <ClearIcon />
        </IconButton>
        {hots.map(hot =>
          <Button key={hot.id} title={hot.id} onClick={this.handleHotClick}>{hot.id}</Button>
        )}
      </ButtonGroup>
    )
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
            <MenuItem value="ob">ob</MenuItem>
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
      : this.renderitemsTable(this.state.items);

    return (
      <div>
        <div>
          <input type="text" onInput={this.handleInput} onKeyPress={this.handlePress} value={this.state.search} />
          <IconButton title="subscribe" onClick={this.handleSubscribeClick}>
            <TurnedInNotIcon />
          </IconButton>
        </div>
        {this.renderHots(this.state.hots)}
        {this.renderSources(this.state.source)}
        {this.renderitemsPagination(this.state.page)}
        {contents}
      </div>
    );
  }

  async getItems(filter) {
    var url = '/api/items';
    var { page = 1, source, search = "" } = filter;
    if (null != filter) {
      url = `${url}?${queryString.stringify(filter)}`;
    }
    // this.setState({ ...this.state, loading: true });
    const response = await fetch(url);
    const data = await response.json();
    this.setState({ ...this.state, items: data, loading: false, page: page, source: source, search: search });
  }

  async getHotsData() {
    var url = '/api/hots';
    const response = await fetch(url);
    const data = await response.json();
    this.setState({ ...this.state, hots: data });
  }

  async subscribe(key) {
    if (key == '') {
      return
    }
    var url = `/api/subscribes/${key}`;
    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'content-type': 'application/json'
      }//,
      // body: JSON.stringify(item)
    });
    const data = await response.json();
  }
}

FetchItem.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(FetchItem);
