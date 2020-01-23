import React, { Component } from 'react';
import IconButton from '@material-ui/core/IconButton';
import FavoriteBorderIcon from '@material-ui/icons/FavoriteBorder';
import FavoriteIcon from '@material-ui/icons/Favorite';

class Favorite extends Component {
    static displayName = Favorite.name;

    constructor(props) {
        super(props);
        this.state = { item: {} };
    }

    componentDidMount() {
        this.setState({ ...this.state, item: this.props.item });
    }

    handleFaverate = (item) => {
        this.addFaverate(item)
    }

    async addFaverate(item) {
        var url = `/api/favorites/${item.id}`;
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'content-type': 'application/json'
            }//,
            // body: JSON.stringify(item)
        });
        item.favorated = !item.favorated;
        this.setState({ ...this.state, item: item });
    }

    render() {
        let { item } = this.state

        return (
            <div>
                {!item.favorated && <IconButton onClick={() => this.handleFaverate(item)}><FavoriteBorderIcon /></IconButton>}
                {item.favorated && <IconButton onClick={() => this.handleFaverate(item)}><FavoriteIcon /></IconButton>}
            </div>
        );
    }
}

export default Favorite;
