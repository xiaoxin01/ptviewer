import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import Tooltip from '@material-ui/core/Tooltip';
import IconButton from '@material-ui/core/IconButton';
import FavoriteBorderIcon from '@material-ui/icons/FavoriteBorder';
import FavoriteIcon from '@material-ui/icons/Favorite';

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
    thunbnail: {
        maxWidth: 75,
    },
    thunbnailLarge: {
        maxWidth: 1200,
        maxHeight: 896,
    },
});

class ItemsTable extends Component {
    static displayName = ItemsTable.name;

    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.renderitemsTable(this.props.items);
    }


    handleFaverate = (item, index) => {
        this.addFaverate(item, index)
    }

    renderitemsTable(items) {
        const { classes } = this.props;
        let [from, to] = [atob('aW1nLm0tdGVhbS5jYw=='), atob('dHBpbWcuY2NhY2hlLm9yZw==')]
        let getImgSrc = (ori, from, to) => {
            if (null === ori) {
                return null
            }
            return ori.replace(from, to)
        }

        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Act</th>
                        <th>Img</th>
                        <th>Title</th>
                        <th>Score</th>
                        <th>Size</th>
                        <th>Source</th>
                        <th>Created</th>
                    </tr>
                </thead>
                <tbody>
                    {items.map((item, index) =>
                        <tr key={item.id}>
                            <td>
                                {!item.favorated && <IconButton onClick={() => this.handleFaverate(item, index)}><FavoriteBorderIcon /></IconButton>}
                                {item.favorated && <IconButton onClick={() => this.handleFaverate(item, index)}><FavoriteIcon /></IconButton>}
                            </td>
                            <td><Tooltip placement="right-end"
                                title={
                                    <React.Fragment>
                                        <img className={classes.thunbnailLarge} src={getImgSrc(item.image, from, to)}></img>
                                    </React.Fragment>
                                }
                                PopperProps={{
                                    popperOptions: {
                                        modifiers: {
                                            preventOverflow: {
                                                enabled: true,
                                                boundariesElement: 'viewport',
                                            },
                                        },
                                    },
                                }}
                            ><img className={classes.thunbnail} src={getImgSrc(item.image, from, to)}></img></Tooltip></td>
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

    render() {
        let contents = this.props.isLoading
            ? <p><em>Loading...</em></p>
            : this.renderitemsTable(this.props.items);

        return (
            <div>
                {contents}
            </div>
        );
    }

    async addFaverate(item, index) {
        var url = `/api/faverates/${item.id}`;
        item.favorated = !item.favorated;
        this.setState(state => {
            let items = state.items;
            items[index] = item

            return { ...state, items: items }
        });
        //const data = await response.json();
    }
}

ItemsTable.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(ItemsTable);
