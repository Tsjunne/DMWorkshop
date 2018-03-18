import * as React from 'react';
import { Modal, Icon, Image, Table, Button, Divider, List } from "semantic-ui-react";
import { connect } from 'react-redux';
import * as Creature from '../model/Creature';
import StatBlock from './StatBlock';

interface CreatureDetailsProps {
    creature: Creature.Creature
}

export class CreatureDetails extends React.Component<CreatureDetailsProps> {
    public render() {
        var saves = Object.keys(this.props.creature.savingThrows).length > 0 ? <List.Item><b>Saving Throws</b> {this.formatSaves(this.props.creature)} </List.Item> : '';
        var skills = Object.keys(this.props.creature.skillModifiers).length > 0 ? <List.Item><b>Skills</b> {this.formatSkills(this.props.creature)} </List.Item> : '';
        var senses = Object.keys(this.props.creature.vision).length > 0 ? <List.Item><b>Senses</b> {this.formatSenses(this.props.creature)} </List.Item> : '';

        return (
            <Modal size='tiny' trigger={<Button icon='id card outline'/>}>
                <Modal.Header icon='id card outline'>{this.props.creature.name}</Modal.Header>
                <Modal.Content image>
                    <Image wrapped size='tiny' src={'/api/creatures/' + this.props.creature.name + '/portrait'} />
                    <Modal.Description>
                        <Table basic='very' fixed singleLine collapsing compact size='small'>
                            <Table.Body>
                                <Table.Row>
                                    <Table.Cell><Icon name='plus' /> {this.props.creature.maxHP}</Table.Cell>
                                    <Table.Cell><Icon name='shield' /> {this.props.creature.ac}</Table.Cell>
                                    <Table.Cell><Icon name='eye' /> {this.props.creature.passivePerception}</Table.Cell>
                                    <Table.Cell><Icon name='paw' /> {this.formatSpeed(this.props.creature)}</Table.Cell>
                                </Table.Row>
                            </Table.Body>
                        </Table>

                        <StatBlock creature={this.props.creature} />
                        <Divider />
                        <List>
                            {saves}
                            {skills}
                            {senses}
                            <List.Item><b>Challenge</b> {this.formatCR(this.props.creature.cr) + ' (' + this.props.creature.xp +' XP)'}</List.Item>
                        </List>
                    </Modal.Description>
                </Modal.Content>
            </Modal>
        );
    }

    formatCR(cr: number): string {
        if (cr == 0.125) return '1/8';
        if (cr == 0.25) return '1/4';
        if (cr == 0.5) return '1/2';

        return cr.toString();
    }

    formatSpeed(creature: Creature.Creature): string {
        var speed = creature.speed['Walk'] + 'ft';

        for (let type in creature.speed) {
            if(type != 'Walk')
            speed += ', ' + type + ' ' + creature.speed[type] + ' ft'
        }

        return speed;
    }


    formatSenses(creature: Creature.Creature): string {
        var senses = [];

        for (let type in creature.vision) {
            senses.push(type + ' ' + creature.vision[type] + ' ft')
        }

        return senses.length > 0 ? senses.reduce((str, x) => str + ', ' + x) : '';
    }

    formatSkills(creature: Creature.Creature): string {
        var skills = [];

        for (let type in creature.skillModifiers) {
            skills.push(type + ' +' + creature.skillModifiers[type])
        }

        return skills.length > 0 ? skills.reduce((str, x) => str + ', ' + x) : '';
    }

    formatSaves(creature: Creature.Creature): string {
        var saves = [];

        for (let type in creature.savingThrows) {
            saves.push(type + ' +' + creature.savingThrows[type])
        }

        return saves.length > 0 ? saves.reduce((str, x) => str + ', ' + x) : '';
    }
}