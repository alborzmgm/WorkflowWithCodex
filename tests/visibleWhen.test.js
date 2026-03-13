import test from 'node:test';
import assert from 'node:assert/strict';
import { evaluateVisibleWhen } from '../src/visibleWhen.js';

test('supports contains operator for array values', () => {
  const visible = evaluateVisibleWhen(
    { field: 'favoriteFruits', operator: 'contains', value: 'other' },
    { favoriteFruits: ['apple', 'other'] }
  );

  assert.equal(visible, true);
});

test('supports hasValue operator for non-empty text and arrays', () => {
  assert.equal(evaluateVisibleWhen({ field: 'name', operator: 'hasValue' }, { name: 'Alex' }), true);
  assert.equal(evaluateVisibleWhen({ field: 'favoriteFruits', operator: 'hasValue' }, { favoriteFruits: [] }), false);
});

test('supports isEmpty operator', () => {
  assert.equal(evaluateVisibleWhen({ field: 'name', operator: 'isEmpty' }, { name: '' }), true);
  assert.equal(evaluateVisibleWhen({ field: 'name', operator: 'isEmpty' }, { name: 'Alex' }), false);
});

test('supports notEquals operator for arrays and strings', () => {
  assert.equal(
    evaluateVisibleWhen(
      { field: 'favoriteFruits', operator: 'notEquals', value: ['apple', 'banana'] },
      { favoriteFruits: ['apple', 'banana'] }
    ),
    false
  );

  assert.equal(evaluateVisibleWhen({ field: 'name', operator: 'notEquals', value: 'John' }, { name: 'Alex' }), true);
});

test('supports complex all/any conditions', () => {
  const rule = {
    all: [
      { field: 'favoriteFruits', operator: 'contains', value: 'other' },
      {
        any: [
          { field: 'notes', operator: 'hasValue' },
          { field: 'details', operator: 'hasValue' }
        ]
      }
    ]
  };

  assert.equal(evaluateVisibleWhen(rule, { favoriteFruits: ['other'], notes: '', details: 'x' }), true);
});
