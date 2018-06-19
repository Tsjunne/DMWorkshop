import * as React from 'react';
import { Image, Table } from "semantic-ui-react";
import * as Model from '../model/Creature';

interface StatBlockProps {
    creature: Model.Creature
}

export default class StatBlock extends React.Component<StatBlockProps>{
    public render() {
        return (
            <Table basic='very' fixed singleLine collapsing compact size='small'>
                <Table.Body>
                    <Table.Row>
                        <Table.Cell><Image avatar src='/images/strength.svg' /></Table.Cell>
                        <Table.Cell><Image avatar src='/images/dexterity.svg' /></Table.Cell>
                        <Table.Cell><Image avatar src='/images/constitution.svg' /></Table.Cell>
                        <Table.Cell><Image avatar src='/images/intelligence.svg' /></Table.Cell>
                        <Table.Cell><Image avatar src='/images/wisdom.svg' /></Table.Cell>
                        <Table.Cell><Image avatar src='/images/charisma.svg' /></Table.Cell>
                    </Table.Row>
                    <Table.Row textAlign='center'>
                        <Table.Cell><b>{this.formatModifier(this.props.creature.modifiers[0])}</b></Table.Cell>
                        <Table.Cell><b>{this.formatModifier(this.props.creature.modifiers[1])}</b></Table.Cell>
                        <Table.Cell><b>{this.formatModifier(this.props.creature.modifiers[2])}</b></Table.Cell>
                        <Table.Cell><b>{this.formatModifier(this.props.creature.modifiers[3])}</b></Table.Cell>
                        <Table.Cell><b>{this.formatModifier(this.props.creature.modifiers[4])}</b></Table.Cell>
                        <Table.Cell><b>{this.formatModifier(this.props.creature.modifiers[5])}</b></Table.Cell>
                    </Table.Row>
                </Table.Body>
            </Table>
            );
    }

    formatModifier(modifier: number): string {
        var prefix = modifier >= 0 ? '+' : '';
        return prefix + modifier.toString();
    }
}