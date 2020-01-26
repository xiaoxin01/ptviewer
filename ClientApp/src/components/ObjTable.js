import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import IconButton from '@material-ui/core/IconButton';
import DeleteOutlineIcon from '@material-ui/icons/DeleteOutline';

const styles = () => ({
});

class ObjTable extends Component {
    static displayName = ObjTable.name;

    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.renderObjTable(this.props.objs);
    }

    renderObjTable(objs) {
        const { handleDelete } = this.props;

        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <tbody>
                    {objs.map((item) =>
                        <tr>
                            <td>
                                <IconButton onClick={() => handleDelete(item)}>
                                    <DeleteOutlineIcon />
                                </IconButton>
                            </td>
                            {Object.keys(item).map(key =>
                                <td>
                                    {item[key]}
                                </td>
                            )}
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.props.isLoading
            ? <p><em>Loading...</em></p>
            : this.renderObjTable(this.props.objs);

        return (
            <div>
                {contents}
            </div>
        );
    }
}

ObjTable.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(ObjTable);
