import * as React from 'react';
import { NavLink, Link } from 'react-router-dom';
import { Menu } from 'semantic-ui-react';

export class MainMenu extends React.Component<{}, {}> {
    public render() {
        return (
            <Menu>
                <Menu.Item as={NavLink} to={'/creatures'} activeClassName='active'>
                    <span className='glyphicon glyphicon-th-list'></span> Creatures
                </Menu.Item>
            </Menu>
        )
    }
}