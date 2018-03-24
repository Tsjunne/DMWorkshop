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
        var senses = Object.keys(this.props.creature.senses).length > 0 ? <List.Item><b>Senses</b> {this.formatSenses(this.props.creature)} </List.Item> : '';
        
        return (
            <Modal size='small' trigger={<Button icon='id card outline'/>}>
                <Modal.Header>
                    <Icon name='id card outline' /> {this.props.creature.name}
                </Modal.Header>
                <Modal.Content>
                    <Image floated='left' size='tiny' src={'/api/creatures/' + this.props.creature.name + '/portrait'} />
                    <Modal.Description>
                        <Table basic='very' fixed singleLine collapsing compact size='small'>
                            <Table.Body>
                                <Table.Row>
                                    <Table.Cell><Icon name='plus' /> {this.props.creature.maxHP}</Table.Cell>
                                    <Table.Cell><Icon name='shield' /> {this.props.creature.ac}</Table.Cell>
                                    <Table.Cell><Icon name='eye' /> {this.props.creature.passivePerception}</Table.Cell>
                                    <Table.Cell><Icon name='paw' /> {this.formatSpeed(this.props.creature)}</Table.Cell>
                                    <Table.Cell><Icon name='trophy' /> {this.formatCR(this.props.creature.cr) + ' (' + this.props.creature.xp + ' XP)'}</Table.Cell>
                                </Table.Row>
                            </Table.Body>
                        </Table>

                        <StatBlock creature={this.props.creature} />
                        <Divider />
                        <List>
                            {saves}
                            {skills}
                            <TypeList label='Damage Vulnerabilities' types={this.props.creature.damageVulnerabilities} />
                            <TypeList label='Damage Resistances' types={this.props.creature.damageResistances.map(x => this.formatDamageType(x))} />
                            <TypeList label='Damage Immunities' types={this.props.creature.damageImmunities.map(x => this.formatDamageType(x))} />
                            <TypeList label='Condition Immunities' types={this.props.creature.conditionImmunities} />
                            {senses}
                        </List>
                        <Divider />
                        {this.props.creature.specialAbilities.map(sa =>
                            <SimpleInfo label={sa.name} info={sa.info} />
                        )}
                        <Actions label='Actions' actions={this.props.creature.attacks.filter(a => !a.reaction)} />
                        <Actions label='Reactions' actions={this.props.creature.attacks.filter(a => a.reaction)} />
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
            if (type != 'Walk')
                speed += ', ' + type.toLowerCase() + ' ' + creature.speed[type] + ' ft'
        }

        return speed;
    }


    formatSenses(creature: Creature.Creature): string {
        var senses = [];

        for (let type in creature.senses) {
            senses.push(type + ' ' + creature.senses[type] + ' ft')
        }

        return senses.length > 0 ? senses.reduce((str, x) => str + ', ' + x).toLowerCase() : '';
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
            saves.push(type.substr(0, 3) + ' +' + creature.savingThrows[type])
        }

        return saves.length > 0 ? saves.reduce((str, x) => str + ', ' + x) : '';
    }

    formatDamageType(type: Creature.DamageType): string {
        if (type == Creature.DamageType.NonMagical) return 'bludgeoning, piercing, and slashing damage from non magical attacks';
        if (type == Creature.DamageType.NonSilvered) return 'bludgeoning, piercing, and slashing damage from non magical weapons that aren’t silvered';

        return type;
    }
}

class Actions extends React.Component<{ label: string, actions: Creature.Attack[] }>{
    public render() {
        if (this.props.actions.length == 0) return (<span/>);

        return (
            <List>
                <Divider horizontal>{this.props.label}</Divider>
                {this.props.actions.map(action =>
                    action.type ? <AttackInfo attack={action} /> : <SimpleInfo label={action.name} info={action.info} />)}
            </List>
        );
    }
}

class TypeList extends React.Component<{ label:string, types: string[] }>{
    public render() {
        return this.props.types.length > 0 ? <List.Item><b>{this.props.label}</b> {this.props.types.reduce((str, x) => str + ', ' + x).toLowerCase()} </List.Item> : <span />;
    }
}

class SimpleInfo extends React.Component<{ label: string, info: string }> {
    public render() {
        return (
            <List.Item><i><b>{this.props.label}.</b></i> {this.props.info.split('\n').map(line => <span>{line}<br /></span>)} <br /></List.Item>
        );
    }
}

class AttackInfo extends React.Component<{ attack: Creature.Attack }> {
    public render() {
        return (
            <List.Item><i><b>{this.props.attack.name}.</b> {this.formatAttackType(this.props.attack.type)}:</i> {this.formatAttack(this.props.attack)} <i>Hit: </i>{this.formatHit(this.props.attack)} {this.props.attack.info ? this.props.attack.info.split('\n').map(line => <span>{line}<br /></span>) : ''} <br /><br /></List.Item>
        );
    }

    formatAttackType(type: Creature.AttackType): string {
        var format = type & Creature.AttackType.Ranged ? 'Ranged ' : 'Melee ';
        format += type & Creature.AttackType.Spell ? 'Spell ' : 'Weapon ';
        format += 'Attack';

        return format
    }

    formatAttack(attack: Creature.Attack): string {
        var format = `+${attack.hit} to hit, `;

        format += attack.type & Creature.AttackType.Ranged ? `range ${attack.range}ft.` : `reach ${attack.range}ft.`

        if (attack.maxRange) {
            format += `/${attack.maxRange}ft.`
        }

        format += ', one target.'

        return format;
    }

    formatHit(attack: Creature.Attack): string {
        var dmg = attack.damage.map(d => `${d.average} (${d.dieCount}d${d.dieSize}${d.bonus ? (d.bonus > 0 ? '+' : '') + d.bonus : ''}) ${d.type.toLowerCase()} damage`)

        return dmg.length > 0 ? dmg.reduce((str, x) => str + ' plus ' + x) + '.' : '';
    }
}