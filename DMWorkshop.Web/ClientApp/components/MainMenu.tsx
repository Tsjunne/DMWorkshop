import * as React from 'react';
import { NavLink, Link } from 'react-router-dom';
import { Menu, Icon } from 'semantic-ui-react';

interface MainMenuProps extends React.HTMLProps<HTMLElement> { }

export class MainMenu extends React.Component<MainMenuProps, {}> {
    public render() {
        return (
            <Menu
                icon="labeled"
                borderless
                widths={3}>
                <Menu.Item as={NavLink} to="/creatures/">
                    <Icon name="id card" />
                    Creatures
					</Menu.Item>
                <Menu.Item as={NavLink} to="/encounters/">
                    <Icon name="sort content descending" />
                    Encounters
					</Menu.Item>
                <Menu.Item as={NavLink} to="/party/">
                    <Icon name="group" />
                    Party
					</Menu.Item>
            </Menu>
        )
    }
}